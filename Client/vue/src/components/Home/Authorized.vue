<template>
    <div>
        <div v-if="isAuthorized">            
            <div>
                Posts:
                <div v-for="post in storePosts" :key="post.id">
                    <Post :post="post"></Post>
                </div>
            </div>
            <div v-if="loading">
                <Loading></Loading>
            </div>
        </div>
        <div v-else>
            <h1>LOG IN LOGIC / BUTTON</h1>
        </div>
    </div>
</template>

<script>
    import Post from  '@/components/Posts/Post.vue'
    import Loading from  '@/components/Shared/Loading.vue'
    import { mapGetters } from 'vuex'
    export default {
        name: 'AuthorizedHome',
        components: {
            Post,
            Loading
        },
        data(){
            return {
                posts: this.$store.state.post.posts,
                loading: true
            }
        },
        computed: {
            ...mapGetters({
                storePosts: 'post/storePosts'
            }),
            isAuthorized(){
            return this.$store.state.authentication.isLoggedIn
            return this.$store.state.authentication.userData != undefined && this.$store.state.authentication.userData.token != undefined && this.$store.state.authentication.userData.token != ""
            return this.$store.state.post.accessToken != "" && this.$store.state.post.accessToken != undefined
            }
        },
        watch: {
            storePosts(newVal) {
                if (newVal == undefined || newVal.length == 0)
                    this.posts = this.$store.state.post.posts
                this.loading = newVal == undefined;
            }        
        },
        mounted(){          
            this.$store.dispatch('post/fetchPosts')
            let storePosts = this.$store.state.post.posts;
            if(storePosts == undefined || storePosts.length == 0){
                this.$store.dispatch('post/fetchPosts')
            }
        }        
    }
</script>

<style scoped>

</style>