<template>
    <div>
        <h2>{{authenticationType}}</h2>
        <form @submit.prevent="handleSubmit">
            <div class="form-group">
                <label for="email">Email</label>
                <input type="text" v-model="email" name="email" class="form-control" :class="{ 'is-invalid': submitted && !email }" />
                <div v-show="submitted && !email" class="invalid-feedback">Email is required</div>
            </div>
            <div class="form-group">
                <label htmlFor="password">Password</label>
                <input type="password" v-model="password" name="password" class="form-control" :class="{ 'is-invalid': submitted && !password }" />
                <div v-show="submitted && !password" class="invalid-feedback">Password is required</div>
            </div>
            <div class="form-group">
                <button class="btn btn-primary" :disabled="loggingIn">{{authenticationType}}</button>
            </div>
        </form>
        <div v-if="loggingIn">
            <Loading></Loading>
        </div>
    </div>
</template>

<script>
import Loading from  '@/components/Shared/Loading.vue'
export default {
    components: {
        Loading
    },
    props: {
        authenticationType: String
    },
    data () {
        return {
            email: '',
            password: '',
            submitted: false
        }
    },
    computed: {
        loggingIn () {
            return this.$store.state.authentication.authenticationInProgress;
        }
    },
    created () {
        // reset login status
        //ToDo: Change this
        if(this.$store.state.authentication.isLoggedIn){
            this.$store.dispatch('authentication/logout');
        }
    },
    methods: {
        handleSubmit (e) {
            this.submitted = true;
            const { email, password } = this;
            if (email && password) {
                if(this.authenticationType.toLowerCase() === "register"){
                    this.$store.dispatch('authentication/register', { email, password });
                }
                this.$store.dispatch('authentication/login', { email, password });
            }
        }
    }
}
</script>

<style scoped>

</style>