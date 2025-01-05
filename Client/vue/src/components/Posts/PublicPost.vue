<template>
    <div>
        <div class="card text-center">
            <div class="card-header yellowish">
                {{author}}
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
    .yellowish {
        background-color: rgb(225, 247, 182);
    }
</style>