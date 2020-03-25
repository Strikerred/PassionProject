import React from 'react'
import { Link } from 'react-router-dom'

export default class Navbar extends React.Component {

    constructor(){
        super()
        this.logOut = this.logOut.bind(this)
    }

    logOut(e){
        this.setState({role: false})
        this.props.auth.logOut()
    }

    render() {   
        return (
            <nav className="navbar">
            <div className="navbar-menu">
                <div className="navbar-start">
                <Link to="/" className="navbar-item">Home</Link>         
                </div>
                <div className="navbar-end">
                    <div className="navbar-item">
                        <div>
                            {this.props.auth.sessionStorage 
                            ? <p>Welcome! {this.props.auth.userName}</p>
                            : <Link to="/login" className="button is-light">Log in</Link>
                            } 
                        </div>
                        <div>
                            {this.props.auth.sessionStorage 
                            ? <p>Welcome! {this.props.auth.userName}</p>
                            : <Link to="/register" className="button is-light">Register</Link>
                            } 
                        </div>
                        <div>
                            {this.props.auth.sessionStorage && (
                                <Link to="/login" onClick = {this.logOut} className="button is-light">Log out</Link>
                            )}                        
                        </div>
                    </div>
                </div>
            </div>
            </nav>
        )
    }
}