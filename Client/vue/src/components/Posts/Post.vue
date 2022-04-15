<template>
    <div>
        <div>
            <h3>{{title}}</h3>
            <small>{{createdOn}}</small>
            <small>{{author}}</small>
            <p>{{content}}</p>
        </div>
        <div>
            <div v-for="comment in comments" :key="comment.id">
                <Comment :comment="comment"></Comment>
            </div>
        </div>
    </div>
</template>

<script>
    import Comment from  '@/components/Posts/Comment.vue'
    import PostsService from '@/services/PostsService.js'
    export default {
        name: "Post",
        components: {
            Comment
        },
        props: {
            post: Object
        },
        data() {
            return {
                id: this.post.Id,
                title: this.post.Title,
                content: this.post.Content,
                author: this.post.AuthorUsername,
                createdOn: this.post.CreatedOn,
                modifiedOn: this.post.ModifiedOn,
                comments: this.post.Comments
            }
        }
    }
</script>

<style scoped>

</style>