<template>
    <div>
        <div class="card text-center">
            <div class="card-header greyish">
                {{title}}
            </div>
            <div class="card-body">
                <h5 class="card-title"></h5>
                <p class="card-text">{{content}}</p>
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
            </div>
            <br>
            <div>
                <div class="container">
                    <div>
                        <div id="accordion-comment">
                            <div class="card">
                            <div class="card-header" id="headingComment">
                                <h5 class="mb-0">
                                <button class="btn btn-link section-header" data-toggle="collapse" data-target="#collapseComment" aria-expanded="false" aria-controls="collapseComment">
                                    <i class="fa fa-hand-o-right" aria-hidden="true"></i> Comments
                                </button>
                                </h5>
                            </div>

                            <div id="collapseComment" class="collapse show" aria-labelledby="headingComment" data-parent="#accordion-comment">
                                <div class="card-body">                                          
                                    <div v-for="comment in comments" :key="comment.id">
                                        <Comment :comment="comment"></Comment>
                                    </div>
                                </div>
                            </div>
                            </div>
                        </div>
                    </div>

            </div>
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
    }
</style>