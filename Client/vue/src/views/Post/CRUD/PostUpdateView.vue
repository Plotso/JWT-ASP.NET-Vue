<template>
  <div>
    <PostModify :operation="'Update'" :post="post"></PostModify>
  </div>
</template>

<script>
import PostModify from '@/components/Posts/CRUD/PostModify.vue';
import router from '@/router/index.js'
import store from '@/store/index.js';
import {ref} from 'vue'
export default {
    name: 'CreatePost',
    props: {
      id: {
          type: Number,
          required: true
      }
    },
    components: {
        PostModify
    },
    setup(props){
        const post = ref(store.state.post.posts.find(p => p.id == props.id));
        return {post};
    },
    mounted(){
        //ToDo: Add validation to verify whether user is post creator or an admin
    },
    beforeCreate(){
        this.$store.dispatch('post/fetchPosts')
        if(!this.$store.state.authentication.isLoggedIn || 
            this.$store.state.authentication.userData == undefined || 
            this.$store.state.authentication.userData.email == undefined){
            router.push('/');
        }
        let storePosts = this.$store.state.post.posts;
        if(storePosts == undefined || storePosts.length == 0 || !storePosts.some(p => p.id == this.id)){
            router.push('/');
        }

        let currentPost = storePosts.find(p => p.id == this.id);
        let isLoggedInUserAuthor = currentPost.authorUsername != this.$store.state.authentication.userData.email;
        let isLoggedInUserAdmin = this.$store.state.authentication.userData.hasAdministrativeRight;
        if(!isLoggedInUserAuthor && !isLoggedInUserAdmin){
            router.push('/');
        }
    }
}
</script>

<style>

</style>