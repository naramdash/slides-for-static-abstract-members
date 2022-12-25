<template>
  <div>
    <slot></slot>
  </div>
</template>

<style scoped>
div {
  position: absolute;
  bottom: 0;
  margin-bottom: 1em;
  font-size: smaller;
}

:slotted(div p) {
  margin-top: 0px;
  margin-bottom: 0.1em;
}
</style>