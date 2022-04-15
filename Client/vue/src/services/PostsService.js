import axios from 'axios'

const postsApi = axios.create({ //ToDo: Public posts   && 1 more for posts && 1 more for comments
    baseURL: process.env.JWTSHOWCASE_POSTS_GATEWAY_ADDRESS,
    withCredentials: false, // This is the default
    headers: {
      Accept: 'application/json',
      'Content-Type': 'application/json'
    }
})

const authApi = axios.create({  //ToDo: Auth
    baseURL: process.env.JWTSHOWCASE_AUTH_API_ADDRESS,
    withCredentials: false, // This is the default
    headers: {
        'Content-Type' : 'application/x-www-form-urlencoded', 
        'Authorization' : 'Basic ' + btoa(process.env.VUE_APP_SPOTIFY_CLIENTID + ':' + process.env.VUE_APP_SPOTIFY_SECRET)
    }
})


const grantTypeBody = new URLSearchParams({'grant_type':'client_credentials'}).toString();

export default{
    // Identity   TODO: Add body for requests
    login(loginModel){
        return authApi.post('/Identity/Login', loginModel);
    },
    register(registerModel){
        return authApi.post('/Identity/Register', registerModel);
    },
    changePassword(changePasswordModel){
        return authApi.put('/Identity/ChangePassword', changePasswordModel);
    },

    // Posts
    getPublicPosts(){
        return postsApi.get('/PublicPosts/GetAll'); //ToDo: Public posts
    },
    getPosts(accessToken){ //ToDo: Add query logic
        return postsApi.get('/Posts/GetAll', {headers: { 'Authorization' : 'Bearer ' + accessToken}});
    },

    // Methods below are not use since the access token is available in the store and should be renew there
    searchTrackByName(name, accessToken) {
        return spotifyAPIClient.get('/search?q=' + name + '&type=track', {headers: { 'Authorization' : 'Bearer ' + accessToken}})
    },
    getCategories() {
        return spotifyAPIClient.get('/browse/categories')
    },
    getFeaturedPlaylists() {
        return spotifyAPIClient.get('/browse/featured-playlists')
    },
    getFeaturedReleases() {
        return spotifyAPIClient.get('/browse/new-releases')
    }
}