import React from 'react';
import NavbarComponent from './components/nav-bar/nav-bar';
import './App.css';
import HomePage from './components/home-page/home-page';

import './../node_modules/bootstrap/dist/css/bootstrap.min.css';
import './../node_modules/react-bootstrap-table2-toolkit/dist/react-bootstrap-table2-toolkit.min.css';
import './../node_modules/react-bootstrap-table-next/dist/react-bootstrap-table2.min.css';

function App() {
  return (
    <div className="App">
      <NavbarComponent />
      <HomePage />
    </div>
  );
}

export default App;
