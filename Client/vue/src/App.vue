<template>
  <Nav :key="navbarKey"></Nav>
  <div v-if="showLoginRegisterButtons">    
    IsLoggedIn = true
  </div>
  <div v-else>
      IsLoggedIn = false
  </div>
  <div v-if="alert.message" :class="`alert ${alert.type}`">{{alert.message}}</div>
  <router-view/>
</template>

<script>

import Nav from  '@/components/Shared/Nav.vue'
import { ref } from 'vue'

export default {
  name: 'app',
  components: {
    Nav
  },
  
      //TODO: THE DATA SECTION BELOW MIGHT BE OBSOLETE
  data() {
    return {
      showLoginRegisterButtons: this.$store.state.authentication.isLoggedIn,
      componentKey: 0
    }
  },
  created() {
    this.$store.watch(
      (state) => {
        console.log(state.authentication.isLoggedIn);
        state.authentication
      },
      (newValue, oldValue) => {
        console.log(`Update isLoggedIn from ${oldValue} to ${newValue}`);
        showLoginRegisterButtons = newValue
      }
    );
  },
  mounted () {
    document.title = "JWTShowcase"
    /* Moved to HomeView.vue
    this.$store.dispatch('post/fetchPublicPosts')
    if(this.$store.state.authentication.isLoggedIn){
      this.$store.dispatch('post/fetchPosts')
    }
    this.$forceUpdate();
    */
  },
  computed: {
      alert () {
          return this.$store.state.alert
      },
      isAuthorized(){
        const condition = this.$store.state.authentication.isLoggedIn != null
        console.log(condition)
        return this.$store.state.authentication.isLoggedIn
      }
  },
  watch: {
      $route (to, from) {
          // clear alert on location change
          this.$store.dispatch('alert/clear');
          this.showLoginRegisterButtons = this.$store.state.authentication.isLoggedIn;
      }, 
      showLoginRegisterButtons(){
        this.forceNavbarRerender();
      },
      //TODO: THOSE BELOW MIGHT BE OBSOLETE
      isAuthorized(newValue, oldValue) {
        console.log("HELLO FROM HERE!!!")
        this.showLoginRegisterButtons = this.$store.state.authentication.isLoggedIn;
      },
      "this.$store.state.authentication.isLoggedIn": {
        handler(newValue, oldValue){
          console.log(`AAAUpdate isLoggedIn from ${oldValue} to ${newValue}`);
        },
        deep: true
      }
  },
  methods: {
    logout() {      
        this.$store.dispatch('authentication/logout');
    },
    forceNavbarRerender() {
      this.navbarKey = this.navbarKey == 0 || this.navbarKey == undefined ? 1 : 0;
    }
  },
}
</script>

<style>
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
}

nav {
  padding: 30px;
}

nav a {
  font-weight: bold;
  color: #2c3e50;
}

nav a.router-link-exact-active {
  color: #42b983;
}
</style>
