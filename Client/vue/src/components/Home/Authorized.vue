<template>
    <div>
        <div v-if="isAuthorized">
            <div v-for="post in posts" :key="post.id">
                <Post :post="post"></Post>
            </div>
        </div>
        <div v-else>
            <h1>LOG IN LOGIC / BUTTON</h1>
        </div>
    </div>
</template>

<script>
    import Post from  '@/components/Posts/Post.vue'
    export default {
        name: 'AuthorizedHome',
        components: {
            Post
        },
        data(){
            return {
                posts: this.$store.state.post.posts
            }
        },
        computed: {
            isAuthorized(){
            return this.$store.state.post.accessToken != "" && this.$store.state.post.accessToken != undefined
            }
        },
        mounted(){            
            this.$store.dispatch('post/fetchPosts')
        }        
    }
</script>

<style scoped>

</style>