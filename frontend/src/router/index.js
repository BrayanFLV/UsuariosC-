import { createRouter, createWebHistory } from "vue-router";
import LoginView from "../views/LoginView.vue";
import UsersView from "../views/UsersView.vue";

const routes = [
  {
    path: "/",
    name: "Login",
    component: LoginView,
  },
  {
    path: "/usuarios",
    name: "Usuarios",
    component: UsersView,
    meta: { requiresAuth: true },
  },
];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

// Protección de ruta
router.beforeEach((to, from, next) => {
  const isLoggedIn = localStorage.getItem("logueado") === "true";

  if (to.meta.requiresAuth && !isLoggedIn) {
    next({ name: "Login" });
  } else {
    next();
  }
});

export default router;
