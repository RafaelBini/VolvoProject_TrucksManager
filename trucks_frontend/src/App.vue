<template>
  <v-app id="app" data-app>
    <Header />
    <Trucks />
  </v-app>
</template>

<script>
import "./styles/global.css";
import Header from "./components/Header";
import Trucks from "./components/Trucks";

export default {
  name: "App",

  components: {
    Header,
    Trucks,
  },
};
</script>
