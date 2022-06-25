import PostsService from '@/services/PostsService.js'

export const namespaced = true

export const state = {
    publicPosts: [],
    posts: [],
    lastFetchDate: "",
    accessToken: ""
}

export const mutations = {
    SET_PUBLIC_POSTS(state, posts) {
        state.publicPosts = posts,
        state.lastFetchDate = new Date().toString();
    },
    SET_POSTS(state, posts) {
        state.publicPosts = posts,
        state.lastFetchDate = new Date().toString();
    }
}

export const actions = {
    fetchPublicPosts({commit}){
        if(hasNHourPassedSinceLastFetch(0)){
            return
        }
        
        PostsService.getPublicPosts()
        .then(response => { return response.data; })
        .then(resultJson => {
            commit('SET_PUBLIC_POSTS', resultJson.posts);
        })
        .catch(error => {
            console.log('Public Posts Fetch Error: ', error);
          });
    },
    fetchPosts({commit}){        
        PostsService.getPosts()
        .then(response => { return response; })
        .then(resultJson => {
            commit('SET_PUBLIC_POSTS', resultJson.posts);
        })
        .catch(error => {
            console.log('Posts Fetch Error: ', error);
          });
    }
}

function hasNHourPassedSinceLastFetch(n){
    if(state.lastFetchDate != ""){
        var currentDate = new Date()
        var millisecondsSinceLastFetch = currentDate - Date.parse(state.lastFetchDate);
        var hoursSinceLastFetch = (millisecondsSinceLastFetch / 1000) / 60 / 60;
        return n == 0 || n >= hoursSinceLastFetch
    }
    return false
}