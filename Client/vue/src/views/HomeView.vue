<template>
  <div class="home">
    <img alt="Vue logo" src="../assets/logo.png">
    <p>
      JWT Showcase with VueJS for client side & ASP.NET 6 microservices solution
    </p>

    <br>
    
    <div class="container">
      <div>
        <div id="accordion">
            <div class="card">
              <div class="card-header" id="headingOne">
                <h5 class="mb-0">
                  <button class="btn btn-link section-header" data-toggle="collapse" data-target="#collapseOne" aria-expanded="true" aria-controls="collapseOne">
                    <i class="fa fa-hand-o-right" aria-hidden="true"></i> Public Posts
                  </button>
                </h5>
              </div>

              <div id="collapseOne" class="collapse show" aria-labelledby="headingOne" data-parent="#accordion">
                <div class="card-body">
                  <div v-if="hasLoadedPublicPosts">
                    <Public></Public>
                    <hr>
                  </div>
                  <div v-else>
                    <Loading></Loading>
                  </div>
                </div>
              </div>
            </div>
            <div class="card" v-if="isAuthorized">
              <div class="card-header" id="headingTwo">
                <h5 class="mb-0">
                  <button class="btn btn-link collapsed section-header" data-toggle="collapse" data-target="#collapseTwo" aria-expanded="false" aria-controls="collapseTwo">
                    <i class="fa fa-hand-o-right" aria-hidden="true"></i> Posts from users
                  </button>
                </h5>
              </div>
              <div id="collapseTwo" class="collapse" aria-labelledby="headingTwo" data-parent="#accordion">
                <div class="card-body">        
                  <Authorized></Authorized>
                </div>
              </div>
            </div>
      </div>
      </div>

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
  },
  mounted () {
    this.$store.dispatch('post/fetchPublicPosts')
    if(this.$store.state.authentication.isLoggedIn){
      this.$store.dispatch('post/fetchPosts')
    }
    this.$forceUpdate();
  }
}
</script>
<style>
  
  .greenish {
        background-color: rgb(168, 250, 113);
    }

  .section-header {
    
    color: rgb(49, 78, 68) !important;
  }
</style>
