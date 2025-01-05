<template>
  <nav>
    <router-link to="/">Home</router-link> |
    <router-link to="/about">About</router-link> |
    <div v-if="isSessionUserAuthenticated">      
      <router-link to="/index" @click="logout">Logout</router-link>
    </div>
    <div v-else>        
      <router-link to="/login">Login</router-link> |
      <router-link to="/register">Register</router-link>
    </div>
  </nav>
</template>

<script>
    import {computed, watch} from 'vue';
    import {useStore, mapState, mapGetters} from "vuex";
    export default {
        name: "Nav",
        setup() {
            const store = useStore();
            //const auth = computed(() => store.state.authenticated)
            const isAuthorized = computed(() => store.state.authentication.isLoggedIn)
            console.log(store)
            const isAuthorized2 = mapState("authentication", ["isLoggedIn"])
            console.log(isAuthorized2)
            var test = isAuthorized;
            const watchIsAuthorize = watch(isAuthorized, () => test = store.state.authentication.isLoggedIn)
            const logout = () => {
                store.dispatch('authentication/logout');
                window.location.reload(); // Refresh/Reload page so that navbar is reloaded
            }
            return {
                isAuthorized,
                isAuthorized2,
                test,
                logout
            }
        },
        computed: {
            ...mapGetters("authentication", ["isSessionUserAuthenticated"])
        },
        watch: {
            isSessionUserAuthenticated: {
                handler(){
                    console.log("SOMETHING CHANGED :00")
                },
                deep: true,
                immediate: true
            },
            isLoggedIn: {
                handler(){
                    console.log("2SOMETHING CHANGED :00")
                },
                deep: true,
                immediate: true
            },
            state: {
                handler(){
                    console.log("3SOMETHING CHANGED :00")
                },
                immediate: true
            }
        }
  }

</script>

<style scoped>

</style>