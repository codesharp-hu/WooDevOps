const ref = Vue.ref;
const watch = Vue.watch;

const app = Vue.createApp({
  template: ` 
  <button @click="runBashScript">Run</button>
  <button @click="getState">Get state</button>
  <h3>outputs</h3>
  <p v-for="output in outputs">{{ output.text }} - {{ dateTimeFormat(output.timestamp) }}</p>
  <h3>state (running: {{ state.running == true ? true : false }}) {{ state.output?.length }}</h3>
  <p v-for="output in state.outputs">{{ output.text }} - {{ dateTimeFormat(output.timestamp) }}</p>
  `,
  name: 'App',
  setup: function () {
    const outputs = ref([]);
    const state = ref(""); 

    const connection = new signalR.HubConnectionBuilder()
      .withUrl("/scriptStateHub")
      .build();

    connection.on("outputReceived", (output) => {
      outputs.value.push(output);
    });

    connection.start().then(() => {
      console.log("Connection started");
    }).catch((error) => {
      console.error(error);
    });

    async function runBashScript() {
      await fetchPost(`api/start`);
    }
    
    async function getState() {
      const resp = await fetchGet(`api/state`);
      state.value = await resp.json();
    }

    async function fetchGet(route) {
      let response = await fetch(route, {
        method: 'GET',
      })
      return response;
    }

    async function fetchPost(route, body) {
      let response = await fetch(route, {
        method: 'POST',
        headers: {
        'Content-Type': 'application/json',
        },
        body: JSON.stringify(body)
      })
      return response;
    }

    function dateTimeFormat(value) {
      let options = {
        year: 'numeric', month: 'numeric', day: 'numeric',
        hour: 'numeric', minute: 'numeric', second: 'numeric',
      };
  
      const date = new Date(value);
      return new Intl.DateTimeFormat('default', options).format(date);
    }

    return { runBashScript, getState, dateTimeFormat, outputs, state }
  },
});

app.mount('#app');