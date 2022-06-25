<template>
  <nav>
    <router-link to="/">Home</router-link> |
    <router-link to="/about">About</router-link> |
    <div v-if="showLoginRegisterButtons">      
      <router-link to="/index" @click="logout">Logout</router-link>
    </div>
    <div v-else>        
      <router-link to="/login">Login</router-link> |
      <router-link to="/register">Register</router-link>
    </div>
  </nav>
  <div v-if="alert.message" :class="`alert ${alert.type}`">{{alert.message}}</div>
  <router-view/>
</template>

<script>
import { thisExpression } from '@babel/types'

export default {
  name: 'app',
  
      //TODO: THE DATA SECTION BELOW MIGHT BE OBSOLETE
  data() {
    return {
      showLoginRegisterButtons: this.$store.state.authentication.isLoggedIn
    }
  },
  mounted () {
    document.title = "JWTShowcase"
    this.$store.dispatch('post/fetchPublicPosts')
    if(this.$store.state.authentication.isLoggedIn){
      this.$store.dispatch('post/fetchPosts')
    }
  },
  computed: {
      alert () {
          return this.$store.state.alert
      },
      isAuthorized(){
        return this.$store.state.authentication.isLoggedIn
      }
  },
  watch: {
      $route (to, from) {
          // clear alert on location change
          this.$store.dispatch('alert/clear');
      }, 
      //TODO: THOSE BELOW MIGHT BE OBSOLETE
      isAuthorized(newValue, oldValue) {
        this.isAuthorizedUser();
        this.showLoginRegisterButtons = this.$store.state.authentication.isLoggedIn;
      },
      showLoginRegisterButtons(){
        this.showLoginRegisterButtons = this.$store.state.authentication.isLoggedIn;
      }
  },
  methods: {
    logout() {      
        this.$store.dispatch('authentication/logout');
    },
      //TODO: THE ROW BELOW MIGHT BE OBSOLETE
    isAuthorizedUser() {
      return this.$store.state.authentication.isLoggedIn
    }
  }
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
