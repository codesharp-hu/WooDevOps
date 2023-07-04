const ref = Vue.ref;
const watch = Vue.watch;

const app = Vue.createApp({
  template: ` 
  <button @click="runBashScript">Run</button>
  <button @click="getState">Get state</button>
  <p>{{ state }}</p>
  `,
  name: 'App',
  setup: function () {   
    const state = ref(""); 
    async function runBashScript() {
      const resp = await fetchPost(`api/start`);
      state.value = await resp.json();
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

    return { runBashScript, getState, state }
  },
});

app.mount('#app');