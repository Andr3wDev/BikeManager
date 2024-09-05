import React, { useEffect } from 'react';
import { useBikeContext } from '../context/BikeContext';
import { Link } from 'react-router-dom';
import styled from 'styled-components';

const StyledButton = styled.button`
  padding: 10px;
  background-color: #4CAF50; // Green color
  color: white;
  border: none;
  cursor: pointer;
  border-radius: 4px; // Optional: for rounded corners
  font-size: 16px; // Optional: adjust the font size
  text-align: center;
  display: inline-block; // Ensure proper alignment
  transition: background-color 0.3s ease;

  &:hover {
    background-color: #45a049; // Slightly darker green on hover
  }
`;

const EditButton = styled(StyledButton)`
  background-color: #007bff; // Blue color for edit
  &:hover {
    background-color: #0056b3; // Darker blue on hover
  }
`;

const DeleteButton = styled(StyledButton)`
  background-color: #dc3545; // Red color for delete
  &:hover {
    background-color: #c82333; // Darker red on hover
  }
`;

const BikeListPage: React.FC = () => {
  const { bikes, fetchBikes, loading, error, deleteBike } = useBikeContext();

  useEffect(() => {
    fetchBikes();
  }, [fetchBikes]);

  const handleDelete = async (id: number) => {
    await deleteBike(id);
    fetchBikes(); // Refresh post deletion
  };

  return (
    <div>
      <h2>Bikes</h2>
      {loading && <p>Loading...</p>}
      {error && <p>{error}</p>}
      <Link to="/add-bike" style={{ textDecoration: 'none' }}>
        <StyledButton>Add Bike</StyledButton>
      </Link>
      <ul>
        {bikes && bikes.length > 0 ? (
          bikes.map(bike => (
            <li key={bike.id}>
              {bike.brand} - {bike.model}
              <Link to={`/edit-bike/${bike.id}`}>
                <EditButton>Edit</EditButton>
              </Link>
              <DeleteButton onClick={() => handleDelete(bike.id)}>Delete</DeleteButton>
            </li>
          ))
        ) : (
          <p>No bikes available</p>
        )}
      </ul>
    </div>
  );
};

export default BikeListPage;
