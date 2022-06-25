import axios from 'axios'

const authApi = axios.create({  
    baseURL: process.env.VUE_APP_JWTSHOWCASE_AUTH_API_ADDRESS,
    withCredentials: false, // This is the default
    headers: {
        Accept: 'application/json',
        'Content-Type': 'application/json'
    }
})

export default{
    // Identity   TODO: Add body for requests
    login(email, password){
        const loginModel = JSON.stringify({ email, password });
        return authApi.post('/Identity/Login', loginModel)     
        .then(response => { return response.data; })
        .then(userData => {
            if(userData){
                localStorage.setItem('tk_ex', JSON.stringify(userData));
            }

            return userData;
        });
    },
    register(email, password){
        const registerModel = JSON.stringify({ email, password });
        return authApi.post('/Identity/Register', registerModel)    
        .then(response => { return response.data; })
        .then(userData => {
            if(userData){
                localStorage.setItem('tk_ex', JSON.stringify(userData));
            }

            return userData;
        });
    },
    changePassword(changePasswordModel){
        return authApi.put('/Identity/ChangePassword', changePasswordModel);
    },
    logout(){
        // remove user from local storage to log user out
        localStorage.removeItem('tk_ex');
    }
}