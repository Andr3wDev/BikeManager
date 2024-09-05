import React, { useEffect, useState } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import { useBikeContext } from '../context/BikeContext';
import { BikeUpdateForm } from '../components/BikeUpdateForm';
import { BikeUpdateDto, BikeDto } from '../dto/bikeDto';

const UpdateBikePage: React.FC = () => {
  const { id } = useParams<{ id?: string }>();
  const { bikes, updateBike, fetchBikes } = useBikeContext();
  const navigate = useNavigate();
  const [currentBike, setCurrentBike] = useState<BikeDto | undefined>(undefined);

  useEffect(() => {
    if (id) {
      const bike = bikes.find(bike => bike.id === Number(id));
      setCurrentBike(bike);
    }
  }, [id, bikes]);

  const initialValues: BikeUpdateDto = currentBike
    ? {
        id: currentBike.id,
        brand: currentBike.brand,
        model: currentBike.model,
        year: currentBike.year,
        ownerEmail: currentBike.ownerEmail
      }
    : {
        id: 0,
        brand: '',
        model: '',
        year: 2022,
        ownerEmail: ''
      };

  const handleSubmit = async (values: BikeUpdateDto) => {
    try {
      if (id) {
        await updateBike(Number(id), values);
        fetchBikes();
        navigate('/bikes');
      }
    } catch (error) {
      console.error('Failed to update bike:', error);
    }
  };

  if (!currentBike) {
    return <div>Loading...</div>;
  }

  return (
    <div>
      <h2>Update Bike</h2>
      <BikeUpdateForm initialValues={initialValues} onSubmit={handleSubmit} />
    </div>
  );
};

export default UpdateBikePage;
