<template>
    <div>
        <div>
            <small>{{createdOn}}</small>
            <br>
            <small>{{author}}</small>
            <p>{{content}}</p>
        </div>
    </div>
</template>

<script>
    export default {
        name: "PublicPost",
        props: {
            post: Object
        },
        data() {
            return {
                id: this.post.id,
                content: this.post.content,
                author: this.post.authorUsername,
                createdOn: this.post.createdOn,
                modifiedOn: this.post.modifiedOn
            }
        }
    }
</script>

<style scoped>
</style>