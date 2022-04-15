<template>
    <div>

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
                id: this.comment.Id,
                content: this.comment.Content,
                author: this.comment.AuthorUsername
                createdOn: this.post.CreatedOn,
                modifiedOn: this.post.ModifiedOn
            }
        }
    }
</script>

<style scoped>

</style>