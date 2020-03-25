import React from 'react';

const API_INVOKE_URL='https://localhost:44363/api';

export default class Cards extends React.Component {
  constructor() {
    super();
    this.state = { 
      cards: [],
      loading: true,
    };
}

componentDidMount(){
  fetch(API_INVOKE_URL+'/card')
  .then(response => response.json())
  .then(data => {
      this.setState({cards: data, loading:false});      
    }); 
}

  render(){
    return (
      <div>
        <div className="card-deck mr-5">
          {this.state.cards.map(card =>
          <div className="card col-xs-3 col-sm-3 col-xl-2 text-white bg-info m-3 p-2" key={card.templateId}>
            <img className="card-img-top" style={{height: "208px"}} src={card.templateUrl} alt="Card image cap" />
            <div className="card-body">
              <h5 className="card-title">{card.gender == 'U' ? "Unisex" : card.gender == 'F' ? "Male" : "Female"}</h5>
              <p className="card-text">Price: $6.00</p>
              <p className="card-text">Package of 25 invitations</p>
            </div>
            <a href={`/card/${card.TemplateId}`} className="badge badge-pill badge-info m-3">Buy</a>
          </div>
          )}
        </div>
      </div>
    )
  }  
} 