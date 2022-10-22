<template>
    <div>
        <div>
            <h3>{{title}}</h3>
            <small>{{createdOn}}</small>
            <small>{{author}}</small>
            <p>{{content}}</p>
        </div>
        <div>
            Comments:
            <br/>
            
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
                id: this.post.id,
                title: this.post.title,
                content: this.post.content,
                author: this.post.authorUsername,
                createdOn: this.post.createdOn,
                modifiedOn: this.post.modifiedOn,
                comments: this.post.comments
            }
        }
    }
</script>

<style scoped>

</style>