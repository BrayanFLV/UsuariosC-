<template>
  <div
    class="modal fade show d-block"
    tabindex="-1"
    role="dialog"
    style="background-color: rgba(0, 0, 0, 0.5)"
  >
    <div class="modal-dialog">
      <div class="modal-content">
        <div class="modal-header">
          <h5 class="modal-title">
            {{ user?.id ? "Editar Usuario" : "Nuevo Usuario" }}
          </h5>
          <button
            type="button"
            class="btn-close"
            @click="$emit('close')"
          ></button>
        </div>

        <div class="modal-body">
          <form @submit.prevent="handleSubmit">
            <div class="mb-3">
              <label>Nombre</label>
              <input v-model="form.nombre" class="form-control" required />
            </div>
            <div class="mb-3">
              <label>Apellidos</label>
              <input v-model="form.apellidos" class="form-control" required />
            </div>
            <div class="mb-3">
              <label>Cédula</label>
              <input v-model="form.cedula" class="form-control" required />
            </div>
            <div class="mb-3">
              <label>Correo</label>
              <input
                v-model="form.correo"
                class="form-control"
                type="email"
                required
              />
            </div>
            <div class="mb-3">
              <label>Fecha Último Acceso</label>
              <input
                v-model="form.fechaUltimoAcceso"
                class="form-control"
                type="datetime-local"
                required
              />
            </div>

            <div class="text-end">
              <button
                class="btn btn-secondary me-2"
                type="button"
                @click="$emit('close')"
              >
                Cancelar
              </button>
              <button type="submit" class="btn btn-success">
                <i class="fas fa-save me-1"></i> Guardar
              </button>
            </div>
          </form>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import axios from "axios";

export default {
  name: "UserFormModal",
  props: {
    user: Object,
    show: Boolean,
  },
  data() {
    return {
      form: {
        nombre: "",
        apellidos: "",
        cedula: "",
        correo: "",
        fechaUltimoAcceso: "",
      },
    };
  },
  watch: {
    user: {
      immediate: true,
      handler(newVal) {
        if (newVal) {
          this.form = { ...newVal };
        } else {
          this.form = {
            nombre: "",
            apellidos: "",
            cedula: "",
            correo: "",
            fechaUltimoAcceso: "",
          };
        }
      },
    },
  },
  methods: {
    async handleSubmit() {
      try {
        if (this.user && this.user.id) {
          await axios.put(
            `http://localhost:5020/api/Users/${this.user.id}`,
            this.form
          );
        } else {
          await axios.post("http://localhost:5020/api/Users", this.form);
        }
        this.$emit("saved");
      } catch (error) {
        console.error("❌ Error al guardar usuario:", error);
        alert("Ocurrió un error al guardar el usuario.");
      }
    },
  },
};
</script>
