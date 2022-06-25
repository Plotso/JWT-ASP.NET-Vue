<template>
    <div>
      <Authentication :authenticationType="'Login'"></Authentication>
    </div>
</template>

<script>
import Authentication from  '@/components/Authentication/Authentication.vue'
export default {
    name: 'LoginView',
    components: {
        Authentication
    }
}
</script>

<style scoped>

</style>