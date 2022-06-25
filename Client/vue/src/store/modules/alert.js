export const namespaced = true

export const state = {
    type: null,
    message: null
}

export const mutations = {
    SUCCESS(state, message) {
        state.type = 'alert-success';
        state.message = message;
    },
    ERROR(state, message) {
        state.type = 'alert-danger';
        state.message = message;
    },
    FAILURE(state) {
        state.type = null;
        state.message = null;
    }
}

export const actions = {
    success({ commit }, message) {
        commit('SUCCESS', message);
    },
    error({ commit }, message) {
        commit('ERROR', message);
    },
    clear({ commit }) {
        commit('FAILURE');
    }
}
