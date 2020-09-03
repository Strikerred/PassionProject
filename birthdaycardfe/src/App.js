import React, { Component } from 'react'
import './App.css'
import { BrowserRouter as Router, Route, Switch, Redirect } from 'react-router-dom'
import Home from './Util/Home'
import Navbar from './Util/Navbar'
import Card from './components/Card'
import Login from './components/Login'
import Register from './components/Register'


const BASE_URL='https://localhost:44363/api/auth';
const API_INVOKE_URL='https://localhost:44363/api/card';

const AUTH_TOKEN = "auth_token";

class App extends Component {

  constructor(){
    super();
    this.state = {
        token: "",
        userName: false,
        loginMessage: ""       
    }
    this.logout = this.logout.bind(this)
    this.login = this.login.bind(this)
    this.register = this.register.bind(this)
    this.submitInfo = this.submitInfo.bind(this);
  }

  componentDidMount() {  
      if(sessionStorage.getItem(AUTH_TOKEN)!=null) {
        this.setState({ 
          token:sessionStorage.getItem(AUTH_TOKEN)});
      }
  }

  login(email, password, rememberMe) {

    const URL = BASE_URL + '/login';
      
    fetch(URL, {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
        },
        body: JSON.stringify({
            Email:      email, 
            Password:   password,
            RememberMe: rememberMe 
        })
    })

    .then(response => response.json())
        .then(json => {
            if(json["status"]==="OK") {
              sessionStorage.setItem(AUTH_TOKEN, json["token"]);
              this.token   = json["token"];
              sessionStorage.setItem("USERNAME", json["userName"])            
              this.userName = json["userName"]
              if(json["role"] === "Admin" || json["role"] === "Manager"){
                sessionStorage.setItem("ROLE", json["role"])
                this.role = json["role"] 
              }else{
                sessionStorage.setItem("ROLE", "Customer")
                this.role = "Customer" 
              }

              this.setState({token: sessionStorage.AUTH_TOKEN})
              this.setState({userName: sessionStorage.USERNAME})
              this.setState({loginMessage:"The user has been logged in."}); 
            }
            else {
              this.setState({loginMessage:
                "An error occured at login. Try again." }); 
            }
        })
        .catch(function (error) {
            if(sessionStorage[""])
            alert(error);
        }) 
  }

  register(username, email, password, confirmPassword) {

    const URL = BASE_URL + '/register';
      
    fetch(URL, {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
        },
        body: JSON.stringify({
            Username:         username,
            Email:            email, 
            Password:         password,
            ConfirmPassword:  confirmPassword
        })
    })

    .then(response => response.json())
        .then(json => {
            if(json["status"]==="OK") {
              sessionStorage.setItem(AUTH_TOKEN, json["token"]);
              this.token   = json["token"];
              sessionStorage.setItem("USERNAME", json["userName"])            
              this.userName = json["userName"]
              if(json["role"] === "Admin" || json["role"] === "Manager"){
                sessionStorage.setItem("ROLE", json["role"])
                this.role = json["role"] 
              }else{
                sessionStorage.setItem("ROLE", "Customer")
                this.role = "Customer" 
              }

              this.setState({token: sessionStorage.AUTH_TOKEN})
              this.setState({userName: sessionStorage.USERNAME})
              this.setState({loginMessage:"The user has been registered and is logged in, please verify your email to have full access."}); 
            }
            else {
              this.setState({loginMessage:
                "An error occured at register. Try again." }); 
            }
        })
        .catch(function (error) {
            if(sessionStorage[""])
            alert(error);
        }) 
  }

  logout() {

      const URL = BASE_URL + '/logout';
      
    fetch(URL, {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
        },
        body: JSON.stringify({})
    })

      this.setState({loginMessage:
        "The user has been logged out." });
      if(sessionStorage.getItem([AUTH_TOKEN])!=null) {
          sessionStorage.clear();
      }
      
      this.setState({token:"", userName: ""});
  }

  // Executes when button pressed.
  submitInfo(e) {
    const URL = API_INVOKE_URL + '/register';
    let token   = sessionStorage.auth_token
    
    fetch(URL, {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${token}`
        },
        body: JSON.stringify({
            IsComplete: false, // Set default to false.
            Description: description,
            UserName: this.props.auth.userName
        })
    })
    // Response received.
    .then(response => response.json())

    // Data retrieved.
    .then(json => {
    this.getAll();
    })
    // Data not retrieved.
    .catch(function (error) {
        console.log(error);
    })
}

  render() {    
    const session = {
      submitInfo: this.submitInfo,
      logIn: this.login,
      logOut: this.logout,
      sessionStorage: sessionStorage.getItem([AUTH_TOKEN]) == null ? false : sessionStorage.getItem([AUTH_TOKEN]),
      userName: sessionStorage.USERNAME,
      BASE_URL,
      loginMessage: this.state.loginMessage
    }

    return  (
    <Router>
      <div>
      <Navbar auth={session}/>
      <Switch>    
        <Route exact path="/" render = {(props) => <Home {...props}/>}/>
        <Route exact path="/login" render = {(props) => <Login {...props} auth = {session} />}/>      
        <Route exact path="/Register" render = {(props) => <Register {...props} auth = {session} />}/>
        <Route path="/card/:id" render = {(props) => <Card {...props} auth = {session}/>}/>                          
      </Switch>
      </div>
    </Router>);
  }
}

export default App;
