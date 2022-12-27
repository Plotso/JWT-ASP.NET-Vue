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

.text-center {
    text-align: center!important;
}

header.masthead {
  position: relative;
  margin-bottom: 3rem;
  padding-top: calc(8rem + 57px);
  padding-bottom: 8rem;
  background: no-repeat center center;
  background-size: cover;
  background-attachment: scroll;
  background-color: #97d67c;
}
header.masthead:before {
  content: "";
  position: absolute;
  top: 0;
  left: 0;
  right: 0;
  bottom: 0;
  background-color: #212529;
  opacity: 0.5;
}
header.masthead .page-heading,
header.masthead .post-heading,
header.masthead .site-heading {
  color: #fff;
}
header.masthead .page-heading,
header.masthead .site-heading {
  text-align: center;
}
header.masthead .page-heading h1, header.masthead .page-heading .h1,
header.masthead .site-heading h1,
header.masthead .site-heading .h1 {
  font-size: 3rem;
}
header.masthead .page-heading .subheading,
header.masthead .site-heading .subheading {
  font-size: 1rem;
  font-weight: 300;
  line-height: 1.1;
  display: block;
  margin-top: 0.625rem;
  font-family: "Open Sans", -apple-system, BlinkMacSystemFont, "Segoe UI", Roboto, "Helvetica Neue", Arial, sans-serif, "Apple Color Emoji", "Segoe UI Emoji", "Segoe UI Symbol", "Noto Color Emoji";
}
header.masthead .post-heading h1, header.masthead .post-heading .h1 {
  font-size: 2.25rem;
}
header.masthead .post-heading .meta,
header.masthead .post-heading .subheading {
  line-height: 1.1;
  display: block;
}
header.masthead .post-heading .subheading {
  font-size: 1rem;
  font-weight: 600;
  margin: 0.75rem 0 2rem;
  font-family: "Open Sans", -apple-system, BlinkMacSystemFont, "Segoe UI", Roboto, "Helvetica Neue", Arial, sans-serif, "Apple Color Emoji", "Segoe UI Emoji", "Segoe UI Symbol", "Noto Color Emoji";
}
header.masthead .post-heading .meta {
  font-size: 1.25rem;
  font-weight: 300;
  font-style: italic;
  font-family: "Lora", -apple-system, BlinkMacSystemFont, "Segoe UI", Roboto, "Helvetica Neue", Arial, sans-serif, "Apple Color Emoji", "Segoe UI Emoji", "Segoe UI Symbol", "Noto Color Emoji";
}
header.masthead .post-heading .meta a {
  color: #fff;
}
@media (min-width: 992px) {
  header.masthead {
    padding-top: 12.5rem;
    padding-bottom: 12.5rem;
  }
  header.masthead .page-heading h1, header.masthead .page-heading .h1,
header.masthead .site-heading h1,
header.masthead .site-heading .h1 {
    font-size: 5rem;
  }
  header.masthead .post-heading h1, header.masthead .post-heading .h1 {
    font-size: 3.5rem;
  }
  header.masthead .post-heading .subheading {
    font-size: 1.375rem;
  }
}
</style>
