<template>
  <div class="home">
    <img alt="Vue logo" src="../assets/logo.png">
    <HelloWorld msg="Welcome to Your Vue.js App"/>

    <br>
    <div>
      <div v-if="hasLoadedPublicPosts">
        <Public></Public>
      </div>
      <div v-else>
        <Loading></Loading>
      </div>
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
import Loading from  '@/components/Shared/Loading.vue'

export default {
  name: 'HomeView',
  components: {
    HelloWorld,
    Public,
    Authorized,
    Loading
  },
  computed: {
    isAuthorized(){
      return this.$store.state.authentication.isLoggedIn
    },
    hasLoadedPublicPosts(){
      var publicPosts = this.$store.state.post.publicPosts;
      return publicPosts != undefined && publicPosts.length > 0
    }
  }
}
</script>
