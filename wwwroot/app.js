const ref = Vue.ref;
const watch = Vue.watch;

const app = Vue.createApp({
  template: ` 
  <button @click="runBashScript">Run</button>
  <p>{{ state }}</p>
  `,
  name: 'App',
  setup: function () {   
    const state = ref(""); 
    async function runBashScript() {
      const resp = await fetchPost(`api/start`);
      state.value = await resp.text();
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

    return { runBashScript, state }
  },
});

app.mount('#app');