<template>
  <div>
    <PostModify :operation="'Create'"></PostModify>
  </div>
</template>

<script>
import PostModify from '@/components/Posts/CRUD/PostModify.vue';
export default {
    name: 'CreatePost',
    components: {
        PostModify
    },
    mounted(){
        if(!this.$store.state.authentication.isLoggedIn){
            router.push('/');
        }
    }
}
</script>

<style>

</style>