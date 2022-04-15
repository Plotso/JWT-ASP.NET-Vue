<template>
    <div>
        <div v-for="post in posts" :key="post.id">
            <PublicPost :post="post"></PublicPost>
        </div>
    </div>
</template>

<script>
    import PublicPost from  '@/components/Posts/PublicPost.vue'

    export default {
        name: 'PublicHome',
        components: {
            PublicPost
        },
        data(){
            return {
                posts: this.$store.state.post.publicPosts
            }
        },
        mounted(){            
            this.$store.dispatch('post/fetchPublicPosts')
        }
    }
</script>

<style scoped>

</style>