<template>
  <div class="home">
    <img alt="Vue logo" src="../assets/logo.png">
    <HelloWorld msg="Welcome to Your Vue.js App"/>

    <br>
    <div>
      <Public></Public>
    </div>
    <div v-if="isAuthorized">
      <Authorized></Authorized>

    </div>
  </div>
</template>

<script>
// @ is an alias to /src
import HelloWorld from '@/components/HelloWorld.vue'
import Public from  '@/components/Home/Public.vue'  //ToDo: Those should be moved inside logged in and non-logged in user pages
import Authorized from  '@/components/Home/Authorized.vue'

export default {
  name: 'HomeView',
  components: {
    HelloWorld,
    Public,
    Authorized
  },
  computed: {
    isAuthorized(){
      return this.$store.state.post.accessToken != "" && this.$store.state.post.accessToken != undefined
    }
  }
}
</script>
