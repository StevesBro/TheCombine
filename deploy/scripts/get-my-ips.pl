#!/usr/bin/perl

################################################################################
# list all non-loopback network interfaces as a list of JSON structures:
# {
#	"ip" : "nnn.nnn.nnn.nnn"
#   "comment" : "Comment" 	
# }
#
# The default comment is "*** FOR CONFIG ***"
#
################################################################################

use strict;

my $ifName;
my $ipAddr;
my @ipAddrList;
my $comment="*** FOR CONFIG ***";

while ($#ARGV > -1)
{
	my $opt = shift;
	if ($opt =~ /-c/)
	{
		$comment = shift;
	}
}

open(IPADDR, "ip -o -4 address |") or die "Cannot list IP addresses";
while (<IPADDR>)
{
	chomp;
	if (/^\d+:\s+(\S+)\s+inet\s(\d+\.\d+\.\d+\.\d+).*/)
	{
		$ifName = $1;
		$ipAddr = $2;
		if ($ifName ne "lo") {
			push @ipAddrList, $ipAddr;
		}
	}
}
close IPADDR;

print STDOUT "[ ";
my $firstIter = 1;

foreach my $addr ( @ipAddrList ) {
	if ($firstIter)
	{
		print STDOUT "{ ";
		$firstIter = 0;
	} else { 
		print STDOUT ", { ";
	}
	
	print STDOUT "\"ip\" : \"$addr\", \"comment\" : \"$comment\" }";
}

print STDOUT " ]\n";

