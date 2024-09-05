import * as Yup from 'yup';

// Combined bike validation. Same as API
export const BikeCreateSchema = Yup.object().shape({
  brand: Yup.string()
    .required('Brand is required'),  
  model: Yup.string()
    .required('Model is required'),
  year: Yup.number()
    .required('Year is required')
    .min(1900, 'Year must be greater than 1900'),
  ownerEmail: Yup.string()
    .email('Invalid email')
    .required('Owner email is required'),
});

export const BikeUpdateSchema = Yup.object().shape({
  id: Yup.number()
    .required(),
  ...BikeCreateSchema.fields,
});
