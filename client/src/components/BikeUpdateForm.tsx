import React from 'react';
import { Formik, Form, Field, ErrorMessage } from 'formik';
import { BikeUpdateDto } from '../dto/bikeDto';
import { BikeUpdateSchema } from '../validations/bikeValidation';
import { StyledForm, StyledInput, StyledButton, ErrorText } from './styles';

interface BikeUpdateFormProps {
  initialValues: BikeUpdateDto;
  onSubmit: (values: BikeUpdateDto) => void;
}

export const BikeUpdateForm: React.FC<BikeUpdateFormProps> = ({ initialValues, onSubmit }) => {
  return (
    <Formik
      initialValues={initialValues}
      validationSchema={BikeUpdateSchema}
      onSubmit={values => onSubmit(values)}
    >
      {() => (
        <StyledForm>
          <div>
            <StyledInput as={Field} name="brand" placeholder="Brand" />
            <ErrorMessage name="brand" component={ErrorText} />
          </div>
          <div>
            <StyledInput as={Field} name="model" placeholder="Model" />
            <ErrorMessage name="model" component={ErrorText} />
          </div>
          <div>
            <StyledInput as={Field} name="year" placeholder="Year" type="number" />
            <ErrorMessage name="year" component={ErrorText} />
          </div>
          <div>
            <StyledInput as={Field} name="ownerEmail" placeholder="Owner Email" />
            <ErrorMessage name="ownerEmail" component={ErrorText} />
          </div>
          <StyledButton type="submit">Update Bike</StyledButton>
        </StyledForm>
      )}
    </Formik>
  );
};
