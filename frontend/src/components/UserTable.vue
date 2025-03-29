<template>
  <div class="table-responsive">
    <table class="table table-bordered table-hover align-middle text-center">
      <thead class="table-dark">
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
        <tr v-for="user in users" :key="user.id">
          <td>{{ user.nombre }} {{ user.apellidos }}</td>
          <td>{{ user.correo }}</td>
          <td>{{ user.cedula }}</td>
          <td>{{ new Date(user.fechaUltimoAcceso).toLocaleString() }}</td>
          <td>
            <span class="badge bg-info text-dark">{{
              user.clasificacion
            }}</span>
          </td>
          <td>
            <strong>{{ user.puntaje }}</strong>
          </td>
          <td>
            <button
              class="btn btn-sm btn-warning me-2"
              @click="$emit('edit-user', user)"
            >
              <i class="fas fa-edit"></i>
            </button>
            <button
              class="btn btn-sm btn-danger"
              @click="$emit('delete-user', user.id)"
            >
              <i class="fas fa-trash-alt"></i>
            </button>
          </td>
        </tr>
      </tbody>
    </table>
  </div>
</template>

<script>
export default {
  name: "UserTable",
  props: {
    users: Array,
  },
};
</script>
