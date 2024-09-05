import React from 'react';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import { Navbar } from './components/Navbar';
import { BikeProvider } from './context/BikeContext';
import { GlobalStyles } from './styles/GlobalStyles';
import BikeListPage from './pages/BikeListPage';
import { HomePage } from './pages/HomePage';
import AddBikePage from './pages/AddBikePage';
import UpdateBikePage from './pages/UpdateBikePage';

const App: React.FC = () => {
  return (
    <BikeProvider>
      <GlobalStyles />
      <Router>
        <Navbar />
        <Routes>
          <Route path="/" element={<HomePage />} />
          <Route path="/bikes" element={<BikeListPage />} />
          <Route path="/add-bike" element={<AddBikePage />} />
          <Route path="/edit-bike/:id" element={<UpdateBikePage />} />
          <Route path="*" element={<HomePage />} />
        </Routes>
      </Router>
    </BikeProvider>
  );
};

export default App;
