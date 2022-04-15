<template>
    <div>
        <div>
            <small>{{createdOn}}</small>
            <small>{{author}}</small>
            <p>{{content}}</p>
        </div>
    </div>
</template>

<script>
    import PostsService from '@/services/PostsService.js'
    export default {
        name: "PublicPost",
        props: {
            post: Object
        },
        data() {
            return {
                id: this.post.Id,
                content: this.post.Content,
                author: this.post.AuthorUsername,
                createdOn: this.post.CreatedOn,
                modifiedOn: this.post.ModifiedOn
            }
        }
    }
</script>

<style scoped>

</style>