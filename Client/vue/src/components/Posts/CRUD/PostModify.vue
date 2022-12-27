<template>
    <div class="container justify-content-center">
        <form @submit.prevent="handleSubmit">
            <div class="form-group">
                <label>Title</label>
                <input v-model="title" placeholder="Post Title" :readonly="isOperation('delete')"/>
                <small>
                    The title of the post.
                </small>
                <span v-show="submitted && (!title || title === ' ')" class="text-danger invalid-feedback">Title is required!</span>
            </div>
            <div class="form-group">
                <label>Content</label>
                <textarea v-model="content" placeholder="Content" :readonly="isOperation('delete')"></textarea>
                <small>
                    The content of the post.
                </small>
                <span v-show="submitted && (!content || content === ' ')" class="text-danger invalid-feedback">Content is required!</span>
            </div>
            <button class="btn btn-primary" :disabled="loading">{{buttonLabel}}</button>
            <button v-if="isOperation('create')" @click="create" class="btn btn-primary" >Create</button>
            <button v-if="isOperation('update')" @click="update" class="btn btn-primary" >Update</button>
            <div v-if="isOperation('delete')">                
                <input type="submit" name="onSubmitAction" class="btn btn-light" value="Go Back" @click="goBack = true"/>
                <input type="submit" name="onSubmitAction" class="btn btn-primary delete-record-btn" value="Delete" onclick="return confirm(`Are you sure?`)"/>
            </div>
        </form>
        <div v-if="loading">
            <Loading></Loading>
        </div>
    </div>
</template>

<script> //ToDo:
        // 1) Decide if we should use 3 separate buttons or 1 (depending on the delete confirm window)
        // 2) Either use prevent submit handler or just use the submit buttons
import { ref, computed, toRefs, toRef } from 'vue'
import router from '@/router/index.js'
import PostsService from '@/services/PostsService.js'
import { thisExpression } from '@babel/types';
    export default {
        props: {
            operation: String, //Create, Update, Delete
            post: Object
        },
        setup(props){
            console.log(props)
            const { id, title, content } = toRefs(props);
            const submitted = ref(false);
            const loading = ref(false);
            const goBack = ref(false);
            const isOperation = computed((desiredOp) => {                
                return desiredOp != null && props.operation.toLowerCase() === desiredOp.toLowerCase()
            });
            const buttonLabel = computed(() => {
                return props.operation.charAt(0).toUpperCase() + props.operation.slice(1);
            })

            function create(){
                //ToDo
                PostService.create({title, content})
                .then(res => {
                    console.log(res);
                    router.push('/');
                })
                .catch(error => {
                    //ToDo: Display error somewhere
                    console.log('Create Post Error: ', error);
                    this.loading = false;
                });
                
            }
            function update(){
                //ToDo
                PostService.update(id, {title, content})
                .then(res => console.log(res))
                .catch(error => {
                    //ToDo: Display error somewhere
                    console.log('Update Post Error: ', error);
                    this.loading = false;
                });
            }
            function deletePost(){
                //ToDo
                PostService.delete(id)
                .then(res => console.log(res))
                .catch(error => {
                    //ToDo: Display error somewhere
                    console.log('Delete Post Error: ', error);
                    this.loading = false;
                });
            }

            function handleSubmit(e){
                this.submitted = true;
                if(this.goBack){
                    if(isOperation('create')){
                        router.push('/');
                    }
                    else{
                        router.push('/'); // ToDo: Route to view post by id
                    }
                }
                if(this.title && this.content){
                    this.loading = true;
                    if(isOperation('create')){
                        create();
                    }
                    if(isOperation('update')){
                        update();
                    }
                    if(isOperation('delete')){
                        deletePost();
                    }
                }
            }


            return { id, title, content, submitted, loading, goBack, isOperation, buttonLabel, create, update, deletePost, handleSubmit };
        },
        mounted(){
            if(this.$store.state.authentication.isLoggedIn){
                router.push('/');
            }
        }
    }
</script>

<style scoped>

</style>