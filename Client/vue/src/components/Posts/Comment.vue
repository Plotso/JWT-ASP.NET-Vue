<template>
    <div>
        <div>
        <div>
            <small>{{createdOn}}</small>
            <small>{{modifiedOn}}</small>
            <small>{{author}}</small>
            <p>{{content}}</p>
        </div>
    </div>
    </div>
</template>

<script>
    import PostsService from '@/services/PostsService.js'
    export default {
        name: "Comment",
        props: {
            comment: Object
        },
        data() {
            return {
                id: this.comment.id,
                content: this.comment.content,
                author: this.comment.authorUsername,
                createdOn: this.comment.createdOn,
                modifiedOn: this.comment.modifiedOn
            }
        }
    }
</script>

<style scoped>

</style>