<template>
  <PostDetails :post="post"></PostDetails>
</template>

<script>
import PostDetails from '@/components/Posts/PostDetails.vue'
import router from '@/router/index.js'
import store from '@/store/index.js';
import {ref} from 'vue'
export default {
    props: {
        id: {
            type: String,
            required: true
        }
    },
    components: {
        PostDetails
    },
    setup(props){
        const post = ref(store.state.post.posts.find(p => p.id == props.id));
        return {post};
    },
    mounted(){
    },
    beforeCreate(){
        this.$store.dispatch('post/fetchPosts')
        if(!this.$store.state.authentication.isLoggedIn){
            router.push('/');
        }
        let storePosts = this.$store.state.post.posts;
        if(storePosts == undefined || storePosts.length == 0 || !storePosts.some(p => p.id == this.id)){
            router.push('/');
        }
    }
}
</script>

<style>

</style>