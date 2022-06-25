import UsersService from '@/services/UsersService.js'
import router from '@/router/index.js'

const user = JSON.parse(localStorage.getItem('tk_ex'));
export const namespaced = true

console.log(user);
export const state = {
    isLoggedIn: user != null,
    userData: user,
    authenticationInProgress: false
}

//TODO: THE GETTERS BELOW MIGHT BE OBSOLETE
export const getters = {
    isSessionUserAuthenticated() {
        return state != undefined && state.isLoggedIn;
    }
}

export const mutations = {
    START_AUTHENTICATION() {
        state.authenticationInProgress = true
    },
    SET_LOGIN_DATA(user) {
        state.isLoggedIn = true,
        state.userData = user,
        state.authenticationInProgress = false
    },
    AUTHENTICATION_FAILURE() {
        state.isLoggedIn = false,
        state.userData = null,
        state.authenticationInProgress = false
    },
    LOGOUT() {
        state.isLoggedIn = false,
        state.userData = null,
        state.authenticationInProgress = false
    }
}

export const actions = {
    login({ dispatch, commit }, { email, password }) {
        UsersService.login(email, password)
            .then(
                user => {
                    commit('SET_LOGIN_DATA', user);
                    router.push('/');
                },
                error => {
                    commit('AUTHENTICATION_FAILURE', error);
                    dispatch('alert/error', error, { root: true });
                }
            );
    },
    register({ dispatch, commit }, { email, password }) {
        UsersService.register(email, password)
            .then(
                user => {
                    commit('SET_LOGIN_DATA', user);
                    router.push('/');
                },
                error => {
                    commit('AUTHENTICATION_FAILURE', error);
                    dispatch('alert/error', error, { root: true });
                }
            );
    },
    logout({ commit }) {
        UsersService.logout();
        commit('LOGOUT');
        router.push('/');
    }

}