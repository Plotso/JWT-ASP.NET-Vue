<template>
    <div>
      <Authentication :authenticationType="'Register'"></Authentication>
    </div>
</template>

<script>
import Authentication from  '@/components/Authentication/Authentication.vue'
export default {
    name: 'RegisterView',
    components: {
        Authentication
    }
}
</script>

<style scoped>

</style>