<template>
    <!-- Page Header-->
    <header class="masthead">
        <div class="container position-relative px-4 px-lg-5">
            <div class="row gx-4 gx-lg-5 justify-content-center">
                <div class="col-md-10 col-lg-8 col-xl-7">
                    <div class="post-heading">
                        <h1>{{title}}</h1>
                        <br/>
                        <h3 class="subheading">By {{author}} <i class="fa fa-user-circle-o" aria-hidden="true"></i></h3>
                        <br/>
                        <span class="meta">
                            Posted <!--by {{author}}-->
                            <!--<a href="#!">{{author}}</a>-->                            
                            on: {{creationDateFormat}} <i class="fa fa-calendar"></i>
                            &middot; 
                            Updated on: {{modifiedOnFormat}} <i class="fa fa-calendar"></i>
                        </span>
                    </div>
                </div>
            </div>
        </div>
    </header>
    <!-- Post Content-->
    <article class="mb-4">
            <div v-if="isAuthorOrAdmin">
                <div style="display: inline-block;">
                    <div class="edit-button" style="display: inline-block;">
                    <button class="button btn-edit">                        
                        <router-link :to="{name: 'edit post', params: {id: post.id}}">Edit <i class="fa fa-pencil-square-o" aria-hidden="true"></i></router-link>
                    </button>
                    </div>
                </div>
                <span> &middot; </span>
                <div class="delete-button" style="display: inline-block;">
                    <button class="button btn-delete">                        
                        <router-link :to="{name: 'delete post', params: {id: post.id}}">Delete <i class="fa fa-trash-o" aria-hidden="true"></i></router-link>
                    </button>
                </div>
                <hr/>
            </div>
            <div class="container px-4 px-lg-5">
                <div class="row gx-4 gx-lg-5 justify-content-center">
                    <div class="col-md-10 col-lg-8 col-xl-7">
                        {{content}}
                    </div>
                </div>
            </div>
            <hr/>
            <div class="container position-relative px-4 px-lg-5">
                <div id="accordion-comment">
                    <div class="card">
                        <div class="card-header" id="headingComment">
                            <h5 class="mb-0">
                            <button class="btn btn-link section-header" data-bs-toggle="collapse" data-bs-target="#collapseComment" aria-expanded="true" aria-controls="collapseComment">
                                <i class="fa fa-comment" aria-hidden="true"></i> Comments
                            </button>
                            </h5>
                        </div>

                        <div id="collapseComment" class="collapse show" aria-labelledby="headingComment" data-bs-parent="#accordion-comment">
                            <div class="card-body">                                          
                                <div v-for="comment in comments" :key="comment.id">
                                    <Comment :comment="comment"></Comment>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
    </article>
    
    <!-- Footer-->
    <footer class="border-top">
        <div class="container px-4 px-lg-5">
            <div class="row gx-4 gx-lg-5 justify-content-center">
                <div class="col-md-10 col-lg-8 col-xl-7">
                    <div class="small text-center text-muted fst-italic">Copyright &copy; JWTShowcase 2022</div>
                </div>
            </div>
        </div>
    </footer>
</template>

<script> //ToDo: Add edit/delete buttons for post creator or an admin
import { reactive, toRefs, computed } from 'vue'
import moment from 'moment';
import Comment from  '@/components/Posts/Comment.vue'
import {ref} from 'vue'
import store from '@/store/index.js';

export default {
    props: {
        post: {
            type: Object,
            required: true
        }
    },
    components: {
        Comment
    },
    setup (props) {
        const state = reactive({
            count: 0,
            title: ref(props.post.title),
            content: ref(props.post.content),
            author: ref(props.post.authorUsername),
            comments: ref(props.post.comments),  //Add computed for the dates,
            isAuthorOrAdmin: props.post.authorUsername == store.state.authentication.userData.email || store.state.authentication.userData.hasAdministrativeRights
        });

        const creationDateFormat = computed(() => {
            return moment(String(props.post.createdOn)).format('hh:mm:ss DD/MM/YYYY') 
        });

        const modifiedOnFormat = computed(() => {
            let lastModified = moment(String(props.post.modifiedOn)).format('hh:mm:ss DD/MM/YYYY')
            return lastModified.includes("0001") ? this.creationDateFormat : lastModified
        });

        return {
            ...toRefs(state), creationDateFormat, modifiedOnFormat
        }
    }
}
</script>

<style scoped>
header.masthead {
  background-image: url('../../assets/blog-details-title-background.jpg');
  background-color: #6c757d;
}


.edit-button a {
    color: #ffffff !important;
    text-decoration: none;
    font-size: 1.1rem
}

.delete-button a {
    color: #ffffff !important;
    text-decoration: none;
    font-size: 1.1rem
}

.btn-edit {
  background-color: #4CAF50; /* Green */
}
.btn-delete {
  background-color: #af4c4c; /* Green */
}

.button {
  transition-duration: 0.4s;
  border-radius: 30px; /* Curve of border corners */
  border: none;
  color: white;
 
 text-transform: uppercase; /* Make letters uppercase */
}
</style>