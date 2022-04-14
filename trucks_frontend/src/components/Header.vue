<template>
  <header>
    <div>TRUCKS MANAGER <v-icon x-large>mdi-truck</v-icon></div>
  </header>
</template>

<script>
export default {
  name: "AppHeader",
};
</script>

<style scoped>
header {
  background-color: var(--color-background-nav);
  color: var(--color-text-nav);
  display: flex;
  align-content: center;
  justify-content: center;
  padding: 20px;
  font-weight: 550;
  font-size: 22px;
}
</style>
