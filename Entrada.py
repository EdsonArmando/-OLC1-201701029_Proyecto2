importar("Funciones.coline");
var r = new Funciones();

log("------Factorial---------");
log("El factorial de 5");
var dos = r.factorial(5);
log(dos);

log("------FIBONNACI---------");
var fibo = r.fibonnacci(0);
for(var i = 0;i<8;i++){
fibo = r.fibonnacci(i);
log(fibo);
}

log("-------------Akerman-------------");
log("Akerman: (3,6)");
var hac = r.ackermann(3,6);
log(hac);

log("-------------Potencia-------------");
log("base: 2 , potencia:5");
var pot = r.potencia(2,5);
log(pot);

log("-------------HANOI-------------");
log("validar: Hanoi(3,1,2,3)");
function void Hanoi(var n, var origen, var auxiliar, var destino){
var un = n-1;
var or = origen;
var aux = auxiliar;
var des = destino;
if(n==1){
log("Mover disco " + origen + " a " + destino);
}else{

Hanoi(un, or, des, aux);
log("Mover disco " + origen + " a " + destino);
Hanoi(un, aux, or, des);
}
}

Hanoi(3,1,2,3);

log("-------------IMPAR-------------");
log("Numero a analizar= 3");
var imp = r.Impar(3);
if(imp==1){
log("Es Verdadero");
}else{
log("No es verdadero");
}

log("-------------hofstaderFemale-------------");
var on = r.hofstaderFemale(0);
for(var i = 0;i<10;i++){
on = r.hofstaderFemale(i);
log(on);
}
log("--------------Total----------------");
log("Nota : " + 50);