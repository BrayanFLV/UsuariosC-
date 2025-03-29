<template>
  <div
    v-if="show"
    class="toast-container position-fixed top-0 end-0 p-3"
    style="z-index: 9999"
  >
    <div class="toast align-items-center text-bg-{{ type }} show" role="alert">
      <div class="d-flex">
        <div class="toast-body">
          {{ message }}
        </div>
        <button
          type="button"
          class="btn-close me-2 m-auto"
          @click="show = false"
        ></button>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "ToastMessage",
  props: {
    message: String,
    type: {
      type: String,
      default: "success", // 'danger', 'info', etc.
    },
  },
  data() {
    return {
      show: true,
    };
  },
  mounted() {
    setTimeout(() => {
      this.show = false;
    }, 3000);
  },
};
</script>
