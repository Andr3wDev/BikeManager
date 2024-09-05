import React from 'react';
import { useNavigate } from 'react-router-dom';
import { useBikeContext } from '../context/BikeContext';
import { BikeAddForm } from '../components/BikeAddForm';
import { BikeCreateDto } from '../dto/bikeDto';

const AddBikePage: React.FC = () => {
  const navigate = useNavigate();
  const { addBike, fetchBikes } = useBikeContext();

  const initialValues: BikeCreateDto = {
    brand: '',
    model: '',
    year: 2022,
    ownerEmail: ''
  };

  const handleSubmit = async (values: BikeCreateDto) => {
    try {
      await addBike(values);
      fetchBikes();
      navigate('/bikes');
    } catch (error) {
      console.error('Failed to add bike:', error);
    }
  };

  return (
    <div>
      <h2>Add New Bike</h2>
      <BikeAddForm initialValues={initialValues} onSubmit={handleSubmit} />
    </div>
  );
};

export default AddBikePage;
