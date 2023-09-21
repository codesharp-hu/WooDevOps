<template>
  <div class="hello">
    <button @click="runBashScript">Run</button>
    <h3>outputs</h3>
    <p v-for="output in outputs" :key="output">{{ output.text }} - {{ output.timestamp }}</p>
  </div>
</template>

<script>
import { ref } from 'vue';
import * as signalR from '@microsoft/signalr';

export default {
  name: 'HelloWorld',
  components: {},
  props: {},
  setup: function () {
    const outputs = ref([]);

    setOutputState();

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
      outputs.value = [];
    }

    async function setOutputState() {
      const resp = await fetchGet(`api/state`);
      outputs.value = await resp.json();
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

    return { runBashScript, dateTimeFormat, outputs }
  }
}
</script>