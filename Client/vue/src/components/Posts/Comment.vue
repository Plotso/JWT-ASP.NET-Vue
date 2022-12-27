<template>
    <div>        
        <div class="card text-center">
            <div class="card-header blueish">
                <i class="fa fa-user-circle-o" aria-hidden="true"></i> {{author}} 
            </div>
            <div class="card-body">
                <h5 class="card-title"></h5>
                <p class="card-text">{{content}}</p>
            </div>
            <div class="card-footer text-muted">
                <i class="fa fa-calendar"></i>
                Created: {{creationDateFormat}} 
                <span>&nbsp &nbsp</span>
                <i class="fa fa-calendar"></i>
                Last modified: {{modifiedOnFormat}}
            </div>
        </div>
    </div>
</template>

<script>
    import moment from 'moment';
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
    .blueish {
        background-color: aqua;
    }
</style>