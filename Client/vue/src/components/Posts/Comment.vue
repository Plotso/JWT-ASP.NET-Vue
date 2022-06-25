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
                id: this.comment.id,
                content: this.comment.content,
                author: this.comment.authorUsername,
                createdOn: this.post.createdOn,
                modifiedOn: this.post.modifiedOn
            }
        }
    }
</script>

<style scoped>

</style>