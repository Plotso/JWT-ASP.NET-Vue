<template>
    <div>
        <div class="card text-center">
            <div class="card-header greyish">
                <router-link :to="{name: 'view post', params: {id: post.id}}">{{title}}</router-link> <i class="fa fa-external-link" aria-hidden="true"></i> 

            </div>
            <div class="card-body">
                <p class="card-text">{{ $filters.str_limit(content,250) }}</p>
            </div>
            <div class="card-footer text-muted">
                <i class="fa fa-user-circle-o" aria-hidden="true"></i>
                {{author}}
                <span>&nbsp &nbsp</span>
                <i class="fa fa-calendar"></i>
                Created: {{creationDateFormat}} 
                <span>&nbsp &nbsp</span>
                <i class="fa fa-calendar"></i>
                Last modified: {{modifiedOnFormat}}
                <span>&nbsp &nbsp</span>
                <i class="fa fa-comment"></i>
                Comments: {{comments.length}}
            </div>
        </div>
    </div>
</template>

<script>
    import moment from 'moment';
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
        },
        computed: {
            creationDateFormat() {
                return moment(String(this.createdOn)).format('hh:mm:ss DD/MM/YYYY')
            },
            modifiedOnFormat(){
                let lastModified = moment(String(this.modifiedOn)).format('hh:mm:ss DD/MM/YYYY')
                return lastModified.includes("0001") ? this.creationDateFormat : lastModified
            }
        }
    }
</script>

<style scoped>

    .greyish {
        background-color: rgb(245, 253, 226);
        font-size: 1.5rem;
        color: #97d67c !important;
    }

    .greyish a {
        color: #97d67c !important;
    }
</style>