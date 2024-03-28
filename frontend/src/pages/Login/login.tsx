import { Container, Grid, Button, TextField, Box } from "@mui/material";

const LoginForm = () => {

    return(

        <div className="bg-color">
            <Container className="loginform-wrapper">
                <div>
                    <Grid container spacing={2}>
                        <Grid item xs={12} sm={12} lg={16} md={12}>
                            <form>
                                <Box component="span" sx={{ p: 2, color: '#1d395d', textAlign: 'left'}}>
                                    <h1>Login</h1>
                                </Box>
                                <Box>
                                    <TextField fullWidth/>
                                    <TextField fullWidth/>
                                    <Button type="submit" fullWidth variant="contained">Submit</Button>
                                </Box>
                            </form>
                        </Grid>
                    </Grid>
                </div>
            </Container>
        </div>
    )
}

export default LoginForm;