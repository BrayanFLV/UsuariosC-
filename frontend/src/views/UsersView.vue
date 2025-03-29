<template>
  <div class="users-bg py-5">
    <div class="container rounded bg-white bg-opacity-0 p-4 shadow-lg">
      <!-- Header -->
      <div
        class="d-flex justify-content-between align-items-center mb-4 border-bottom pb-3"
      >
        <h2 class="m-0 text-primary fw-bold">Usuarios</h2>
        <button class="btn btn-danger shadow-sm" @click="logout">
          <i class="fas fa-sign-out-alt me-2"></i> Cerrar sesión
        </button>
      </div>

      <!-- Search and Create -->
      <div
        class="d-flex flex-column flex-md-row justify-content-between align-items-center mb-4 gap-3"
      >
        <input
          type="text"
          class="form-control shadow-sm"
          placeholder="Buscar por nombre o correo..."
          v-model="searchQuery"
        />
        <button class="btn btn-primary shadow-sm" @click="openCreateModal">
          <i class="fas fa-user-plus me-2"></i> Nuevo Usuario
        </button>
      </div>

      <!-- Tabla de usuarios -->
      <div class="table-responsive">
        <table class="table table-hover table-striped shadow-sm">
          <thead class="table-primary text-center">
            <tr>
              <th>Nombre</th>
              <th>Correo</th>
              <th>Cédula</th>
              <th>Último Acceso</th>
              <th>Clasificación</th>
              <th>Puntaje</th>
              <th>Acciones</th>
            </tr>
          </thead>
          <tbody>
            <tr v-for="user in filteredUsers" :key="user.id">
              <td>{{ user.nombre }} {{ user.apellidos }}</td>
              <td>{{ user.correo }}</td>
              <td>{{ user.cedula }}</td>
              <td>{{ formatFecha(user.fechaUltimoAcceso) }}</td>
              <td>
                <span class="badge" :class="getBadgeClass(user.clasificacion)">
                  {{ user.clasificacion }}
                </span>
              </td>
              <td>
                <i class="fas fa-star text-warning me-1"></i>
                <strong>{{ user.puntaje }}</strong>
              </td>
              <td class="text-center">
                <div class="d-flex justify-content-center gap-2">
                  <button
                    class="btn btn-sm btn-outline-primary shadow-sm"
                    @click="openEditModal(user)"
                  >
                    <i class="fas fa-edit"></i>
                  </button>
                  <button
                    class="btn btn-sm btn-outline-danger shadow-sm"
                    @click="deleteUser(user.id)"
                  >
                    <i class="fas fa-trash"></i>
                  </button>
                </div>
              </td>
            </tr>
          </tbody>
        </table>
      </div>

      <!-- Modales -->
      <UserFormModal
        v-if="showModal"
        :show="showModal"
        :user="selectedUser"
        @close="showModal = false"
        @saved="handleUserSaved"
      />

      <!-- Toast -->
      <ToastMessage
        v-if="toastMessage"
        :message="toastMessage"
        :type="toastType"
      />
    </div>
  </div>
</template>

<script>
import axios from "axios";
import UserFormModal from "@/components/UserFormModal.vue";
import ToastMessage from "@/components/ToastMessage.vue";

export default {
  name: "UsersView",
  components: {
    UserFormModal,
    ToastMessage,
  },
  data() {
    return {
      users: [],
      showModal: false,
      selectedUser: null,
      toastMessage: "",
      toastType: "success",
      searchQuery: "",
    };
  },
  computed: {
    filteredUsers() {
      const q = this.searchQuery.toLowerCase();
      return this.users.filter(
        (u) =>
          u.nombre.toLowerCase().includes(q) ||
          u.apellidos.toLowerCase().includes(q) ||
          u.correo.toLowerCase().includes(q)
      );
    },
  },
  methods: {
    async fetchUsers() {
      try {
        const res = await axios.get("http://localhost:5020/api/Users");
        this.users = res.data;
      } catch (err) {
        this.toast("Error al obtener usuarios", "danger");
      }
    },
    openCreateModal() {
      this.selectedUser = null;
      this.showModal = true;
    },
    openEditModal(user) {
      this.selectedUser = { ...user };
      this.showModal = true;
    },
    async deleteUser(id) {
      if (confirm("¿Estás seguro de eliminar este usuario?")) {
        try {
          await axios.delete(`http://localhost:5020/api/Users/${id}`);
          this.toast("Usuario eliminado correctamente", "danger");
          this.fetchUsers();
        } catch (err) {
          this.toast("Error al eliminar usuario", "danger");
        }
      }
    },
    handleUserSaved() {
      this.showModal = false;
      this.toast("Usuario guardado correctamente", "success");
      this.fetchUsers();
    },
    toast(message, type = "success") {
      this.toastMessage = "";
      this.toastType = type;
      this.$nextTick(() => {
        this.toastMessage = message;
      });
    },
    formatFecha(fecha) {
      return new Date(fecha).toLocaleString("es-ES", {
        dateStyle: "medium",
        timeStyle: "short",
      });
    },
    getBadgeClass(clasificacion) {
      switch (clasificacion) {
        case "Hechicero":
          return "bg-success text-white";
        case "Luchador":
          return "bg-primary text-white";
        case "Explorador":
          return "bg-warning text-dark";
        case "Olvidado":
          return "bg-secondary text-white";
        default:
          return "bg-light text-dark";
      }
    },
    logout() {
      localStorage.removeItem("logueado");
      this.$router.push("/");
    },
  },
  mounted() {
    this.fetchUsers();
  },
};
</script>

<style scoped>
/* Fondo de la página */
.users-bg {
  background: linear-gradient(135deg, #02003a, #001147, #01014c);
  min-height: 100vh;
  display: flex;
  align-items: center;
  justify-content: center;
}

/* Tabla */
.table {
  border-radius: 0.5rem;
  overflow: hidden;
}

.table th,
.table td {
  vertical-align: middle;
}

.table-hover tbody tr:hover {
  background-color: rgba(0, 123, 255, 0.1);
}

/* Botones */
button.btn {
  border-radius: 0.5rem;
}

/* Input */
input.form-control {
  min-width: 250px;
  border-radius: 0.5rem;
}

/* Badges */
.badge {
  font-size: 0.85rem;
  padding: 0.4em 0.6em;
}
</style>
