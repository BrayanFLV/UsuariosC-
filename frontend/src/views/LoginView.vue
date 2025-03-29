<template>
  <div
    class="login-bg d-flex justify-content-center align-items-center min-vh-100"
  >
    <div class="card shadow-lg p-4 rounded-4" style="min-width: 400px">
      <h3 class="text-center mb-4 text-primary fw-bold">Iniciar Sesión</h3>

      <form @submit.prevent="handleLogin">
        <div class="mb-3">
          <label for="correo" class="form-label fw-semibold">Correo</label>
          <input
            id="correo"
            v-model="correo"
            type="email"
            class="form-control shadow-sm"
            placeholder="correo@ejemplo.com"
            required
          />
        </div>
        <div class="mb-3">
          <label for="password" class="form-label fw-semibold"
            >Contraseña</label
          >
          <input
            id="password"
            v-model="password"
            type="password"
            class="form-control shadow-sm"
            placeholder="********"
            required
          />
        </div>
        <button class="btn btn-primary w-100 shadow-sm" type="submit">
          <i class="fas fa-sign-in-alt me-2"></i> Ingresar
        </button>
      </form>

      <div v-if="error" class="alert alert-danger mt-3 text-center">
        {{ error }}
      </div>
    </div>
  </div>
</template>

<script>
import axios from "axios";

export default {
  name: "LoginView",
  data() {
    return {
      correo: "",
      password: "",
      error: "",
    };
  },
  methods: {
    async handleLogin() {
      if (this.correo && this.password) {
        try {
          await axios.put("http://localhost:5020/api/Users/login", {
            correo: this.correo,
          });

          localStorage.setItem("logueado", "true");
          this.error = "";
          this.$router.push("/usuarios");
        } catch (err) {
          this.error = "Usuario no encontrado o error al acceder.";
        }
      } else {
        this.error = "Correo y contraseña son obligatorios.";
      }
    },
  },
};
</script>

<style scoped>
/* Fondo de la página */
.login-bg {
  background: linear-gradient(135deg, #1b1b2f, #162447, #1f4068);
  min-height: 100vh;
  display: flex;
  align-items: center;
  justify-content: center;
}

/* Tarjeta */
.card {
  border-radius: 1rem;
  background: rgba(255, 255, 255, 0.9);
  box-shadow: 0 4px 10px rgba(0, 0, 0, 0.2);
}

/* Botón */
button.btn {
  border-radius: 0.5rem;
}

/* Input */
input.form-control {
  border-radius: 0.5rem;
  padding: 0.75rem;
}

/* Títulos */
h3 {
  font-weight: bold;
}
</style>
